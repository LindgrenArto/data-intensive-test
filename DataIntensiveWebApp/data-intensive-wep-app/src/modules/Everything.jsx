import React, { useEffect, useMemo, useState } from "react";

const API = "https://localhost:7158";
const url = {
  customers: (db) => `${API}/api/customer/${db}`,
  sites:     (db) => `${API}/api/site/${db}`,
  devices:   (db) => `${API}/api/device/${db}`,
  measures:  (db) => `${API}/api/measurement/${db}`,
  users:      (db) => `${API}/api/user/${db}`
};

export default function Everything() {
  const [db, setDb] = useState(1);

  const [customers, setCustomers] = useState([]);
  const [sites, setSites] = useState([]);
  const [devices, setDevices] = useState([]);
  const [measures, setMeasures] = useState([]);

  const [selCustomer, setSelCustomer] = useState(null);
  const [selSite, setSelSite] = useState(null);
  const [selDevice, setSelDevice] = useState(null);

  const [editing, setEditing] = useState(null);
  const [loading, setLoading] = useState(false);
  const [err, setErr] = useState("");

const fetchJson = async (u, signal) => {
  const r = await fetch(u, { signal });
  const ct = r.headers.get("content-type") || "";
  if (r.status === 204) return []; 
  const text = await r.text();
  if (!r.ok) throw new Error(text || `${r.status}`);
  if (!ct.includes("application/json")) throw new Error(text.slice(0, 200));
  return JSON.parse(text);
};

  // Fetch ALL for current DB
  useEffect(() => {
  const ac = new AbortController();
  setErr(""); setLoading(true);

  (async () => {
    try {
      setCustomers([]);
      setSites([]);
      setDevices([]);
      setMeasures([]);
      setSelCustomer(null);
      setSelSite(null);
      setSelDevice(null);

      const [c, s, d, m] = await Promise.allSettled([
        fetchJson(url.customers(db), ac.signal),
        fetchJson(url.sites(db), ac.signal),
        fetchJson(url.devices(db), ac.signal),
        fetchJson(url.measures(db), ac.signal),
      ]);

      if (c.status === "fulfilled") setCustomers(c.value); else setErr((e) => e || c.reason?.message || "Customers failed");
      if (s.status === "fulfilled") setSites(s.value);
      if (d.status === "fulfilled") setDevices(d.value);
      if (m.status === "fulfilled") setMeasures(m.value);

      setSelCustomer(null); setSelSite(null); setSelDevice(null);
    } finally {
      setLoading(false);
    }
  })();

  return () => ac.abort();
}, [db]);

// Set editing to null if any data changes
useEffect(() => {
  setEditing(null);
}, [db, selCustomer, selSite, selDevice]);

  // Build lookup maps
  const sitesByCustomer = useMemo(() => groupBy(sites, x => x.customerUuid), [sites]);
  const devicesBySite   = useMemo(() => groupBy(devices, x => x.siteUuid), [devices]);
  const measuresByDev   = useMemo(() => groupBy(measures, x => x.deviceUuid), [measures]);

  // Filtered lists for current selections
  const sitesForCustomer = selCustomer ? (sitesByCustomer.get(selCustomer.customerUuid) || []) : [];
  const devicesForSite   = selSite ? (devicesBySite.get(selSite.siteUuid) || []) : [];
  const measuresForDev   = selDevice ? (measuresByDev.get(selDevice.deviceUuid) || []) : [];

  // Save handler
  const onSave = async () => {
    if (!editing) return;
    const { kind, data } = editing;
    const map = {
      customer: `${API}/api/customer/${db}`,
      site:     `${API}/api/site/${db}`,
      device:   `${API}/api/device/${db}`,
      measurement: `${API}/api/measurement/${db}`,
      user: `${API}/api/user/${db}`,
    };
    try {
      setLoading(true); setErr("");
      const r = await fetch(map[kind], {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });
      if (!r.ok) throw new Error(await r.text());

      // Refresh only the affected collection
      if (kind === "customer") setCustomers(await fetchJson(url.customers(db)));
      if (kind === "site")     setSites(await fetchJson(url.sites(db)));
      if (kind === "device")   setDevices(await fetchJson(url.devices(db)));
      if (kind === "measurement") setMeasures(await fetchJson(url.measures(db)));
      if (kind === "user" && selCustomer) {
      setSites(await fetchJson(url.sites(db)));
    }

      setEditing(null);
    } catch (e) { setErr(e.message); }
    finally { setLoading(false); }
  };

  return (
    <div style={{ padding: 24, maxWidth: 1100, margin: "0 auto", fontFamily: "system-ui" }}>
      <h1>Data Explorer</h1>

      <label>
        DB:&nbsp;
        <select value={db} onChange={(e) => setDb(Number(e.target.value))}>
          <option value={1}>One</option>
          <option value={2}>Two</option>
          <option value={3}>Three</option>
        </select>
      </label>
      {loading && <span style={{ marginLeft: 12 }}>Loading…</span>}
      {err && <p style={{ color: "crimson" }}>{err}</p>}

      {/* Customers */}
      <h2 style={{ marginTop: 16 }}>Customers</h2>
      <table border="1" cellPadding="6" width="100%">
        <thead><tr><th>Uuid</th><th>Name</th><th>City</th><th></th></tr></thead>
        <tbody>
        {customers.map(c => (
          <tr key={c.customerUuid} style={{ background: selCustomer?.customerUuid === c.customerUuid ? "#eef" : "transparent" }}>
            <td>{c.customerUuid}</td>
            <td>{c.name}</td>
            <td>{c.city}</td>
            <td style={{ display: "flex", gap: 8 }}>
              <button onClick={() => { setSelCustomer(c); setSelSite(null); setSelDevice(null); }}>Select</button>
              <button onClick={() => setEditing({ kind: "customer", data: { ...c } })}>Edit</button>
            </td>
          </tr>
        ))}
        </tbody>
      </table>

      {/* Sites */}
      <h2 style={{ marginTop: 16 }}>Sites {selCustomer && <>for <em>{selCustomer.name}</em></>}</h2>
      <table border="1" cellPadding="6" width="100%">
        <thead><tr><th>SiteUuid</th><th>Name</th><th>City</th><th>Users (count)</th><th></th></tr></thead>
  <tbody>
  {sitesForCustomer.map(s => (
    <>
      <tr key={s.siteUuid} style={{ background: selSite?.siteUuid === s.siteUuid ? "#eef" : "transparent" }}>
        <td>{s.siteUuid}</td>
        <td>{s.name}</td>
        <td>{s.city}</td>
        <td>{Array.isArray(s.users) ? s.users.length : 0}</td>
        <td style={{ display: "flex", gap: 8 }}>
          <button onClick={() => { setSelSite(s); setSelDevice(null); }}>Select</button>
          <button onClick={() => setEditing({ kind: "site", data: { ...s, customerUuid: selCustomer.customerUuid } })}>Edit</button>
        </td>
      </tr>

      {Array.isArray(s.users) && s.users.length > 0 && (
        <tr>
          <td colSpan={5}>
            <details>
              <summary>{s.users.length} users</summary>
              <ul style={{ margin: 0, paddingLeft: 20, listStyle: "none" }}>
                {s.users.map(u => (
                  <li key={u.userUuid ?? u.id} style={{ display: "flex", gap: 12, alignItems: "center", padding: "4px 0" }}>
                    <span style={{ minWidth: 220 }}>
                      {u.name} — {u.location}
                    </span>
                    <button
                      onClick={() =>
                        setEditing({
                          kind: "user",
                          data: {
                            ...u,
                            siteUuid: s.siteUuid,
                          }
                        })
                      }
                    >
                      Edit
                    </button>
                  </li>
                ))}
              </ul>
            </details>
          </td>
        </tr>
      )}
    </>
  ))}
</tbody>
      </table>

      {/* Devices */}
      <h2 style={{ marginTop: 16 }}>Devices {selSite && <>for <em>{selSite.name}</em></>}</h2>
      <table border="1" cellPadding="6" width="100%">
        <thead><tr><th>DeviceUuid</th><th>Name</th><th>Location</th><th></th></tr></thead>
        <tbody>
        {devicesForSite.map(d => (
          <tr key={d.deviceUuid} style={{ background: selDevice?.deviceUuid === d.deviceUuid ? "#eef" : "transparent" }}>
            <td>{d.deviceUuid}</td>
            <td>{d.name}</td>
            <td>{d.location}</td>
            <td style={{ display: "flex", gap: 8 }}>
              <button onClick={() => setSelDevice(d)}>Select</button>
              <button onClick={() => setEditing({ kind: "device", data: { ...d, siteUuid: selSite.siteUuid } })}>Edit</button>
            </td>
          </tr>
        ))}
        </tbody>
      </table>

      {/* Measurements */}
      <h2 style={{ marginTop: 16 }}>Measurements {selDevice && <>for <em>{selDevice.name}</em></>}</h2>
      <table border="1" cellPadding="6" width="100%">
        <thead><tr><th>MeasurementUuid</th><th>Name</th><th>Measurement</th><th>Location</th><th></th></tr></thead>
        <tbody>
        {measuresForDev.map(m => (
          <tr key={m.measurementUuid}>
            <td>{m.measurementUuid}</td>
            <td>{m.name}</td>
            <td>{m.measurement1}</td>
            <td>{m.location}</td>
            <td><button onClick={() => setEditing({ kind: "measurement", data: { ...m, deviceUuid: selDevice.deviceUuid } })}>Edit</button></td>
          </tr>
        ))}
        </tbody>
      </table>

      {/* Editor */}
      {editing && (
        <div style={{ marginTop: 20, border: "1px solid #ddd", padding: 12 }}>
          <Editor editing={editing} setEditing={setEditing} onSave={onSave} />
        </div>
      )}
    </div>
  );
}

function groupBy(arr, keyFn) {
  const map = new Map();
  for (const item of arr || []) {
    const k = keyFn(item);
    if (!map.has(k)) map.set(k, []);
    map.get(k).push(item);
  }
  return map;
}

function Editor({ editing, setEditing, onSave }) {
  const { kind, data } = editing;
  const fields = {
    customer: ["name", "city"],
    site: ["name", "city", "customerUuid"],
    device: ["name", "location", "siteUuid"],
    measurement: ["name", "measurement1", "location", "deviceUuid"],
    user: ["name", "location", "siteUuid"],
  }[kind];

  const onChange = (k, v) => setEditing({ ...editing, data: { ...data, [k]: v } });

  return (
    <form onSubmit={(e) => { e.preventDefault(); onSave(); }} style={{ display: "grid", gap: 8 }}>
      {fields.map(f => (
        <label key={f} style={{ display: "flex", gap: 8 }}>
          <span style={{ width: 140 }}>{f}</span>
          <input value={data[f] ?? ""} onChange={(e) => onChange(f, e.target.value)} />
        </label>
      ))}
      <div style={{ display: "flex", gap: 8 }}>
        <button type="submit">Save</button>
        <button type="button" onClick={() => setEditing(null)}>Cancel</button>
      </div>
    </form>
  );
}
