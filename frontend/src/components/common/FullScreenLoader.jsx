function FullScreenLoader() {
  return (
    <div className="flex min-h-screen items-center justify-center bg-app-bg">
      <div className="h-10 w-10 animate-spin rounded-full border-4 border-slate-200 border-t-primary" />
    </div>
  );
}

export default FullScreenLoader;
