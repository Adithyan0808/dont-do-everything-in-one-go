export const AUTH_STORAGE_KEYS = {
  token: 'mch_access_token',
  user: 'mch_user',
  rememberMe: 'mch_remember_me',
};

function getStorage(rememberMe = true) {
  return rememberMe ? localStorage : sessionStorage;
}

export function readRememberMe() {
  return localStorage.getItem(AUTH_STORAGE_KEYS.rememberMe) === 'true';
}

export function readStoredSession() {
  const rememberMe = readRememberMe();
  const storage = getStorage(rememberMe);
  const token = storage.getItem(AUTH_STORAGE_KEYS.token);
  const userValue = storage.getItem(AUTH_STORAGE_KEYS.user);

  if (!token) {
    return { token: null, user: null, rememberMe };
  }

  try {
    return { token, user: userValue ? JSON.parse(userValue) : null, rememberMe };
  } catch {
    clearSession();
    return { token: null, user: null, rememberMe: false };
  }
}

export function persistSession({ token, user, rememberMe }) {
  clearSession();
  localStorage.setItem(AUTH_STORAGE_KEYS.rememberMe, rememberMe ? 'true' : 'false');
  const storage = getStorage(rememberMe);
  storage.setItem(AUTH_STORAGE_KEYS.token, token);
  storage.setItem(AUTH_STORAGE_KEYS.user, JSON.stringify(user));
  localStorage.setItem('authToken', token);
}

export function updateStoredUser(user) {
  const rememberMe = readRememberMe();
  getStorage(rememberMe).setItem(AUTH_STORAGE_KEYS.user, JSON.stringify(user));
}

export function clearSession() {
  localStorage.removeItem(AUTH_STORAGE_KEYS.token);
  localStorage.removeItem(AUTH_STORAGE_KEYS.user);
  localStorage.removeItem(AUTH_STORAGE_KEYS.rememberMe);
  localStorage.removeItem('authToken');
  sessionStorage.removeItem(AUTH_STORAGE_KEYS.token);
  sessionStorage.removeItem(AUTH_STORAGE_KEYS.user);
}

export function getAccessToken() {
  return localStorage.getItem(AUTH_STORAGE_KEYS.token) || sessionStorage.getItem(AUTH_STORAGE_KEYS.token);
}
