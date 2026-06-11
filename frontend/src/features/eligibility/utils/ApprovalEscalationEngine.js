export function shouldEscalate(approval) {
  if (!approval) return false;
  const sla = (approval.slaRemaining || '').toString().toLowerCase();
  if (sla.includes('breached')) return true;
  const m = sla.match(/(\d+)h/);
  if (m) {
    const hours = parseInt(m[1], 10);
    // escalate if less than 48 hours for high priority items
    if (hours < 48 && (approval.priority || '').toLowerCase() === 'high') return true;
  }
  return false;
}

export default shouldEscalate;
