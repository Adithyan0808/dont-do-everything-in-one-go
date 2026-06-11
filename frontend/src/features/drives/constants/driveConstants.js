export const DRIVE_STATUSES = ['Draft', 'Active', 'Closed', 'Archived'];

export const DRIVE_STATUS_STYLES = {
  Draft: 'bg-slate-100 text-slate-700',
  Active: 'bg-emerald-100 text-emerald-700',
  Closed: 'bg-amber-100 text-amber-700',
  Archived: 'bg-red-100 text-red-700',
};

export const VENDOR_CATALOG = {
  AWS: ['Cloud Practitioner', 'Solutions Architect Associate', 'Security Specialty'],
  Azure: ['Azure Fundamentals', 'Azure Administrator Associate', 'Azure Solutions Architect Expert'],
  'Google Cloud': ['Cloud Digital Leader', 'Associate Cloud Engineer', 'Professional Cloud Architect'],
  HashiCorp: ['Terraform Associate', 'Vault Associate'],
  Kubernetes: ['CKA', 'CKAD', 'CKS'],
  RedHat: ['RHCSA', 'RHCE'],
  Snowflake: ['SnowPro Core', 'SnowPro Advanced Architect'],
  Databricks: ['Data Engineer Associate', 'Machine Learning Associate'],
  Terraform: ['Terraform Associate'],
  Cisco: ['CCNA', 'CCNP Enterprise'],
};

export const DISTRIBUTION_STRATEGIES = [
  'First Come First Serve',
  'Approval Based',
  'Performance Based',
  'Manual Assignment',
];

export const driveQueryOptions = {
  staleTime: 60 * 1000,
  gcTime: 5 * 60 * 1000,
  retry: 1,
  refetchOnReconnect: true,
};

export const driveKeys = {
  all: ['drives'],
  detail: (driveId) => ['drives', 'detail', driveId],
  dashboard: (driveId) => ['drives', 'dashboard', driveId],
  analytics: (driveId) => ['drives', 'analytics', driveId],
  registrations: (driveId) => ['drives', 'registrations', driveId],
  vouchers: (driveId) => ['drives', 'vouchers', driveId],
};
