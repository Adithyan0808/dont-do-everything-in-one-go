function TableSkeleton() {
  return (
    <div className="space-y-2">
      {[1, 2, 3, 4, 5].map((item) => (
        <div key={item} className="h-12 animate-pulse rounded-md bg-slate-100" />
      ))}
    </div>
  );
}

export default TableSkeleton;
