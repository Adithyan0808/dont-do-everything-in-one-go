/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,jsx}'],
  theme: {
    extend: {
      colors: {
        primary: '#4F46E5',
        success: '#10B981',
        warning: '#F59E0B',
        danger: '#EF4444',
        'app-bg': '#F8FAFC',
        card: '#FFFFFF',
      },
      borderRadius: {
        card: '12px',
      },
      fontSize: {
        xs: ['0.75rem', { lineHeight: '1rem' }],
        sm: ['0.875rem', { lineHeight: '1.25rem' }],
        base: ['1rem', { lineHeight: '1.5rem' }],
        lg: ['1.125rem', { lineHeight: '1.75rem' }],
        xl: ['1.25rem', { lineHeight: '1.75rem' }],
      },
      spacing: {
        18: '4.5rem',
        72: '18rem',
      },
      boxShadow: {
        sm: '0 1px 2px 0 rgb(15 23 42 / 0.06)',
        md: '0 8px 20px -12px rgb(15 23 42 / 0.35)',
      },
    },
  },
  plugins: [],
};
