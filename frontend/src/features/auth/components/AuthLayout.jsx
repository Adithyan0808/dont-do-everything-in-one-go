function AuthLayout({ children, title, subtitle }) {
  return (
    <main className="flex min-h-screen items-center justify-center bg-app-bg px-4 py-8">
      <div className="w-full max-w-md">
        <div className="mb-6 text-center">
          <div className="mx-auto mb-4 flex h-12 w-12 items-center justify-center rounded-card bg-primary text-lg font-bold text-white">
            M
          </div>
          <h1 className="text-2xl font-semibold text-slate-950">Maverick Certification Hub</h1>
          <p className="mt-2 text-sm font-medium text-slate-700">{title}</p>
          <p className="mt-1 text-sm text-slate-500">{subtitle}</p>
        </div>
        {children}
        <footer className="mt-6 text-center text-xs text-slate-500">
          <p>Version 1.0.0</p>
          <p className="mt-1">&copy; 2026 Maverick Systems</p>
        </footer>
      </div>
    </main>
  );
}

export default AuthLayout;
