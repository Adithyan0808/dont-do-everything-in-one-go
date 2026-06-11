export const ELIGIBILITY_STATES = [
  'Eligible',
  'ConditionallyEligible',
  'PendingApproval',
  'Rejected',
  'Ineligible',
];

export const RULE_STATUSES = {
  Passed: 'Passed',
  Failed: 'Failed',
  Warning: 'Warning',
  Skipped: 'Skipped',
};

export const DECISION_STYLES = {
  Eligible: 'bg-emerald-50 border-emerald-200 text-emerald-800',
  ConditionallyEligible: 'bg-amber-50 border-amber-200 text-amber-800',
  PendingApproval: 'bg-blue-50 border-blue-200 text-blue-800',
  Rejected: 'bg-red-50 border-red-200 text-red-800',
  Ineligible: 'bg-red-50 border-red-200 text-red-800',
};

export const eligibilityKeys = {
  all: ['eligibility'],
  detail: (registrationId) => ['eligibility', 'detail', registrationId],
  rules: (registrationId) => ['eligibility', 'rules', registrationId],
  approvals: ['eligibility', 'approvals'],
  history: (registrationId) => ['eligibility', 'history', registrationId],
};

export const eligibilityQueryOptions = {
  staleTime: 60 * 1000,
  gcTime: 5 * 60 * 1000,
  retry: 1,
  refetchOnReconnect: true,
};
