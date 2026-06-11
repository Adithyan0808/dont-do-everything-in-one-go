import CreateDriveWizard from '../forms/CreateDriveWizard';

function CreateDrivePage() {
  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h1 className="text-2xl font-semibold text-slate-950">Launch Certification Drive</h1>
        <p className="mt-1 text-sm text-slate-500">Configure program details, eligibility, vouchers, budget, and review before publishing.</p>
      </section>
      <CreateDriveWizard />
    </div>
  );
}

export default CreateDrivePage;
