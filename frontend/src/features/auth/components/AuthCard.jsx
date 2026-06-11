function AuthCard({ children }) {
  return (
    <div className="w-full max-w-md rounded-card border border-slate-200 bg-card p-6 shadow-md sm:p-8">
      {children}
    </div>
  );
}

export default AuthCard;
