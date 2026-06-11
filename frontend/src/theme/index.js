export const theme = {
  colors: {
    primary: '#4F46E5',
    success: '#10B981',
    warning: '#F59E0B',
    danger: '#EF4444',
    background: '#F8FAFC',
    card: '#FFFFFF',
  },
  radius: {
    card: '12px',
  },
  shadows: {
    sm: 'shadow-sm',
    md: 'shadow-md',
  },
};

export function ThemeProvider({ children }) {
  return children;
}
