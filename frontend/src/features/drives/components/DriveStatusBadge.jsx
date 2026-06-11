import { DRIVE_STATUS_STYLES } from '../constants/driveConstants';

function DriveStatusBadge({ status }) {
  return (
    <span className={`inline-flex rounded-full px-2.5 py-1 text-xs font-semibold ${DRIVE_STATUS_STYLES[status] || DRIVE_STATUS_STYLES.Draft}`}>
      {status}
    </span>
  );
}

export default DriveStatusBadge;
