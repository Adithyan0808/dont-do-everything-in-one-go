function LoadingSkeleton({ className = '' }) {
  return <div className={`animate-pulse rounded-md bg-slate-200 ${className || 'h-10 w-full'}`} />;
}

export default LoadingSkeleton;
