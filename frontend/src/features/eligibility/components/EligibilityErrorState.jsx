function EligibilityErrorState({ title = 'Eligibility data unavailable', error, onRetry }) {
  return (
    <div className="rounded-card border border-red-200 bg-red-50 p-5 text-sm text-red-800">
      <p className="font-semibold">{title}</p>
      <p className="mt-1">We could not load the eligibility section. Please retry.</p>
      {error?.message && <p className="mt-2 font-mono text-xs">{error.message}</p>}
      {onRetry && <button type="button" className="mt-3 rounded-md bg-white px-3 py-2 font-medium" onClick={onRetry}>Retry</button>}
    </div>
  );
}

export default EligibilityErrorState;
