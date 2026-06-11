import { z } from 'zod';

export const registerSchema = z
  .object({
    employeeId: z.string().min(1, 'Employee ID is required'),
    fullName: z.string().min(1, 'Full name is required'),
    email: z.string().min(1, 'Email is required').email('Enter a valid email address'),
    department: z.string().min(1, 'Department is required'),
    businessUnit: z.string().min(1, 'Business unit is required'),
    location: z.string().min(1, 'Location is required'),
    password: z
      .string()
      .min(8, 'Password must be at least 8 characters')
      .regex(/[A-Z]/, 'Password must include an uppercase letter')
      .regex(/[a-z]/, 'Password must include a lowercase letter')
      .regex(/[0-9]/, 'Password must include a number')
      .regex(/[^A-Za-z0-9]/, 'Password must include a special character'),
    confirmPassword: z.string().min(1, 'Confirm your password'),
    role: z.enum(['Admin', 'Coordinator', 'Approver', 'Candidate']),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: 'Passwords must match',
    path: ['confirmPassword'],
  });
