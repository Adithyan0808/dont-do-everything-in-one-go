import LoadingSkeleton from './LoadingSkeleton';

function PageLoader() {
  return (
    <div className="space-y-4 p-6">
      <LoadingSkeleton className="h-8 w-64" />
      <LoadingSkeleton className="h-40 w-full" />
      <LoadingSkeleton className="h-40 w-full" />
    </div>
  );
}

export default PageLoader;
