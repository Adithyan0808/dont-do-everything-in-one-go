import { z } from 'zod';

export const examPreferenceSchema = z.object({
  examTrack: z.string().min(1, 'Exam track is required'),
  preferredDate: z.string().min(1, 'Preferred date is required'),
  preferredTime: z.string().min(1, 'Preferred time is required'),
  preferredTimeZone: z.string().min(1, 'Time zone is required'),
  priorAttempts: z.coerce.number().int().min(0),
  specialAccommodations: z.string().optional(),
});

export const registrationWizardSchema = z
  .object({
    driveId: z.string().min(1, 'Select a certification drive'),
    employeeId: z.string().min(1, 'Employee ID is required'),
    fullName: z.string().min(1, 'Name is required'),
    email: z.string().email('Valid email is required'),
    department: z.string().min(1, 'Department is required'),
    businessUnit: z.string().min(1, 'Business unit is required'),
    location: z.string().min(1, 'Location is required'),
    managerEmail: z.string().email('Manager email is required'),
  })
  .merge(examPreferenceSchema);

export const approvalSchema = z.object({
  comments: z.string().min(2, 'Comments are required'),
});

export const statusLookupSchema = z
  .object({
    registrationId: z.string().optional(),
    employeeId: z.string().optional(),
    email: z.string().email('Enter a valid email').optional().or(z.literal('')),
  })
  .refine((data) => data.registrationId || data.employeeId || data.email, {
    message: 'Enter Registration ID, Employee ID, or Email',
    path: ['registrationId'],
  });
