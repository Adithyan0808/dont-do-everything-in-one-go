import { Funnel, FunnelChart, LabelList, ResponsiveContainer, Tooltip } from 'recharts';

function DriveFunnelChart({ drive }) {
  const data = [
    { stage: 'Registered', value: drive.currentRegistrationCount || 0 },
    { stage: 'Eligible', value: drive.approvedCount || 0 },
    { stage: 'Approved', value: drive.approvedCount || 0 },
    { stage: 'Exam Taken', value: drive.examTakenCount || 0 },
    { stage: 'Passed', value: drive.passedCount || 0 },
    { stage: 'Voucher Assigned', value: drive.voucherAssignedCount || 0 },
    { stage: 'Certified', value: drive.passedCount || 0 },
  ];

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <h2 className="text-lg font-semibold text-slate-950">Conversion Funnel</h2>
      <div className="mt-4 h-72">
        <ResponsiveContainer width="100%" height="100%">
          <FunnelChart>
            <Tooltip />
            <Funnel dataKey="value" data={data} fill="#4F46E5">
              <LabelList position="right" fill="#334155" stroke="none" dataKey="stage" />
            </Funnel>
          </FunnelChart>
        </ResponsiveContainer>
      </div>
    </section>
  );
}

export default DriveFunnelChart;
