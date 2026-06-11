export function formatPercent(value) {
  return `${Math.round(value || 0)}%`;
}

export function formatNumber(value) {
  return new Intl.NumberFormat('en-US').format(value || 0);
}

export function formatCurrency(value) {
  return new Intl.NumberFormat('en-IN', {
    style: 'currency',
    currency: 'INR',
    maximumFractionDigits: 0,
  }).format(value || 0);
}

export function getSlaTone(compliance) {
  if (compliance >= 95) return 'success';
  if (compliance >= 90) return 'warning';
  return 'danger';
}
