import { useState } from 'react';
import { registrationFeatureApi } from '../api/registrationApi';

function RegistrationStatusLookup() {
  const [query, setQuery] = useState('');
  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleLookup = async (e) => {
    e?.preventDefault();
    if (!query) return;
    setLoading(true);
    try {
      const res = await registrationFeatureApi.lookupStatus({ registrationId: query });
      setResult(res);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="rounded-card border border-slate-200 bg-white p-4">
      <form onSubmit={handleLookup} className="flex gap-2">
        <input aria-label="Registration ID" placeholder="Registration ID" value={query} onChange={(e) => setQuery(e.target.value)} className="flex-1 rounded-md border px-3 py-2" />
        <button type="submit" className="rounded-md bg-primary px-3 py-2 text-white">Lookup</button>
      </form>

      {loading && <p className="mt-3 text-sm text-slate-500">Searching…</p>}

      {result && (
        <div className="mt-3 rounded-card border p-3 bg-slate-50">
          <p className="font-semibold">{result.registrationId} — {result.status}</p>
          <p className="text-sm text-slate-600">{result.certificationName} · {result.driveName}</p>
        </div>
      )}
    </div>
  );
}

export default RegistrationStatusLookup;
