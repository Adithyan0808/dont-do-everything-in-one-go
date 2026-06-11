export default function EligibilitySkeleton() {
  return (
    <div className="space-y-3">
      <div className="h-6 w-1/3 rounded bg-slate-200 animate-pulse" />
      <div className="grid gap-3">
        <div className="h-24 rounded bg-slate-100 animate-pulse" />
        <div className="h-24 rounded bg-slate-100 animate-pulse" />
        <div className="h-24 rounded bg-slate-100 animate-pulse" />
      </div>
    </div>
  );
}
