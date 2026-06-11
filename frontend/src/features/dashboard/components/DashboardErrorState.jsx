function DashboardErrorState({ title = 'Dashboard data unavailable', error, onRetry }) {
  return (
    <div className="rounded-card border border-red-200 bg-red-50 p-4 text-sm text-red-800">
      <p className="font-semibold">{title}</p>
      <p className="mt-1">We could not load this dashboard section. Please retry.</p>
      {error?.message && <p className="mt-2 font-mono text-xs text-red-700">{error.message}</p>}
      {onRetry && (
        <button type="button" className="mt-3 rounded-md bg-white px-3 py-1.5 font-medium text-red-800" onClick={onRetry}>
          Retry
        </button>
      )}
    </div>
  );
}

export default DashboardErrorState;
