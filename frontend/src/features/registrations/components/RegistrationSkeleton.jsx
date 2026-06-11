function RegistrationSkeleton() {
  return (
    <div className="animate-pulse rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="h-4 w-28 rounded bg-slate-200" />
      <div className="mt-4 h-6 w-3/4 rounded bg-slate-200" />
      <div className="mt-4 h-3 rounded bg-slate-200" />
      <div className="mt-6 grid grid-cols-3 gap-3">
        <div className="h-12 rounded bg-slate-200" />
        <div className="h-12 rounded bg-slate-200" />
        <div className="h-12 rounded bg-slate-200" />
      </div>
    </div>
  );
}

export default RegistrationSkeleton;
