function ProgressBar({ current = 0, target = 100, label = 'Progress' }) {
  const percent = target > 0 ? Math.min(100, Math.round((current / target) * 100)) : 0;

  return (
    <div aria-label={`${label}: ${percent}%`}>
      <div className="flex justify-between text-xs text-slate-500">
        <span>{current}/{target}</span>
        <span>{percent}%</span>
      </div>
      <div className="mt-1 h-2 overflow-hidden rounded-full bg-slate-100">
        <div className="h-full rounded-full bg-primary transition-all duration-500" style={{ width: `${percent}%` }} />
      </div>
    </div>
  );
}

export default ProgressBar;
