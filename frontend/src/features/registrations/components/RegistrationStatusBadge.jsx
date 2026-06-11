import { STATUS_STYLES } from '../constants/registrationConstants';

function RegistrationStatusBadge({ status }) {
  return <span className={`inline-flex rounded-full px-2.5 py-1 text-xs font-semibold ${STATUS_STYLES[status] || STATUS_STYLES.Pending}`}>{status}</span>;
}

export default RegistrationStatusBadge;
