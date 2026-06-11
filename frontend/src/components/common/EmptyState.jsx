function EmptyState({ title = 'No records found', description = 'There is nothing to show yet.' }) {
  return (
    <div className="rounded-card border border-dashed border-slate-300 bg-card p-8 text-center">
      <h3 className="text-sm font-semibold text-slate-900">{title}</h3>
      <p className="mt-1 text-sm text-slate-500">{description}</p>
    </div>
  );
}

export default EmptyState;
