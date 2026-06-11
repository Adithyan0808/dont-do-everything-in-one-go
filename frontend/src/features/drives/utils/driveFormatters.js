export const formatCurrency = (value) =>
  new Intl.NumberFormat('en-IN', { style: 'currency', currency: 'INR', maximumFractionDigits: 0 }).format(value || 0);

export const formatNumber = (value) => new Intl.NumberFormat('en-US').format(value || 0);

export const formatPercent = (value) => `${Math.round(value || 0)}%`;

export const buildDriveCode = (vendorName, certificationName) => {
  const vendor = (vendorName || 'DRV').replace(/\s+/g, '').slice(0, 4).toUpperCase();
  const cert = (certificationName || 'CERT').replace(/[^a-zA-Z0-9]/g, '').slice(0, 5).toUpperCase();
  return `${vendor}-${cert}-${new Date().getFullYear()}`;
};

export const getBudgetUtilization = (drive) => (drive?.budgetAllocated ? Math.round(((drive.budgetConsumed || 0) / drive.budgetAllocated) * 100) : 0);
