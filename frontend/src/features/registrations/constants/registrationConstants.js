export const REGISTRATION_STATUSES = [
  'Pending',
  'EligibilityCheck',
  'PendingApproval',
  'Approved',
  'Rejected',
  'ExamScheduled',
  'ExamTaken',
  'ResultSubmitted',
  'Passed',
  'Failed',
  'VoucherAssigned',
  'VoucherRedeemed',
  'Certified',
  'Ineligible',
  'Cancelled',
];

export const JOURNEY_STAGES = [
  { key: 'submitted', label: 'Registration Submitted', statuses: ['Pending', 'EligibilityCheck', 'PendingApproval', 'Approved', 'ExamScheduled', 'ExamTaken', 'ResultSubmitted', 'Passed', 'Failed', 'VoucherAssigned', 'VoucherRedeemed', 'Certified'] },
  { key: 'eligibility', label: 'Eligibility Validation', statuses: ['EligibilityCheck', 'PendingApproval', 'Approved', 'ExamScheduled', 'ExamTaken', 'ResultSubmitted', 'Passed', 'Failed', 'VoucherAssigned', 'VoucherRedeemed', 'Certified'] },
  { key: 'approval', label: 'Approval', statuses: ['PendingApproval', 'Approved', 'ExamScheduled', 'ExamTaken', 'ResultSubmitted', 'Passed', 'Failed', 'VoucherAssigned', 'VoucherRedeemed', 'Certified'] },
  { key: 'schedule', label: 'Exam Scheduling', statuses: ['ExamScheduled', 'ExamTaken', 'ResultSubmitted', 'Passed', 'Failed', 'VoucherAssigned', 'VoucherRedeemed', 'Certified'] },
  { key: 'exam', label: 'Exam Completion', statuses: ['ExamTaken', 'ResultSubmitted', 'Passed', 'Failed', 'VoucherAssigned', 'VoucherRedeemed', 'Certified'] },
  { key: 'result', label: 'Result Submission', statuses: ['ResultSubmitted', 'Passed', 'Failed', 'VoucherAssigned', 'VoucherRedeemed', 'Certified'] },
  { key: 'voucher', label: 'Voucher Assignment', statuses: ['VoucherAssigned', 'VoucherRedeemed', 'Certified'] },
  { key: 'certified', label: 'Certification Completion', statuses: ['Certified'] },
];

export const STATUS_STYLES = {
  Pending: 'bg-slate-100 text-slate-700',
  EligibilityCheck: 'bg-blue-100 text-blue-700',
  PendingApproval: 'bg-amber-100 text-amber-700',
  Approved: 'bg-emerald-100 text-emerald-700',
  Rejected: 'bg-red-100 text-red-700',
  ExamScheduled: 'bg-blue-100 text-blue-700',
  ExamTaken: 'bg-indigo-100 text-indigo-700',
  ResultSubmitted: 'bg-indigo-100 text-indigo-700',
  Passed: 'bg-emerald-100 text-emerald-700',
  Failed: 'bg-red-100 text-red-700',
  VoucherAssigned: 'bg-purple-100 text-purple-700',
  VoucherRedeemed: 'bg-purple-100 text-purple-700',
  Certified: 'bg-emerald-100 text-emerald-700',
  Ineligible: 'bg-red-100 text-red-700',
  Cancelled: 'bg-slate-100 text-slate-700',
};

export const registrationKeys = {
  all: ['registrations'],
  mine: (userId) => ['registrations', 'mine', userId],
  detail: (registrationId) => ['registrations', 'detail', registrationId],
  timeline: (registrationId) => ['registrations', 'timeline', registrationId],
  eligibility: (registrationId) => ['registrations', 'eligibility', registrationId],
  audit: (registrationId) => ['registrations', 'audit', registrationId],
  communications: (registrationId) => ['registrations', 'communications', registrationId],
  approvals: ['registrations', 'approvals'],
};

export const registrationQueryOptions = {
  staleTime: 60 * 1000,
  gcTime: 5 * 60 * 1000,
  retry: 1,
  refetchOnReconnect: true,
};
