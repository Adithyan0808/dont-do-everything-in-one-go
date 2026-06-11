export function resolveApprovalPriority(item) {
  // Simple heuristic: breached SLA or high priority -> Critical
  if (!item) return 'Medium';
  const sla = (item.slaRemaining || '').toString().toLowerCase();
  if (sla.includes('breached')) return 'Critical';
  if (item.priority === 'High') return 'High';
  // If less than 6 hours remaining mark High
  const m = sla.match(/(\d+)h/);
  if (m && parseInt(m[1], 10) <= 6) return 'High';
  return item.priority || 'Medium';
}

export default resolveApprovalPriority;
