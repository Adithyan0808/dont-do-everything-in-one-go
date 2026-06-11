function ErrorState({ title = 'Something went wrong', description, onRetry }) {
  return (
    <div className="rounded-card border border-danger/20 bg-red-50 p-4">
      <h3 className="text-sm font-semibold text-red-900">{title}</h3>
      {description && <p className="mt-1 text-sm text-red-700">{description}</p>}
      {onRetry && (
        <button type="button" className="mt-3 text-sm font-medium text-red-800 underline" onClick={onRetry}>
          Retry
        </button>
      )}
    </div>
  );
}

export default ErrorState;
