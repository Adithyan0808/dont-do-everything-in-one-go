import { create } from 'zustand';

export const useAppStore = create((set) => ({
  isSidebarCollapsed: false,
  isMobileSidebarOpen: false,
  theme: 'light',
  notificationCount: 0,
  preferences: {},
  toggleSidebar: () => set((state) => ({ isSidebarCollapsed: !state.isSidebarCollapsed })),
  setMobileSidebarOpen: (isOpen) => set({ isMobileSidebarOpen: isOpen }),
  setTheme: (theme) => set({ theme }),
  setNotificationCount: (notificationCount) => set({ notificationCount }),
  setPreference: (key, value) =>
    set((state) => ({
      preferences: {
        ...state.preferences,
        [key]: value,
      },
    })),
}));
