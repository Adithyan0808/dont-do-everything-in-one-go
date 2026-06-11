import { z } from 'zod';

export const timelineSchema = z
  .object({
    registrationStartDate: z.string().min(1, 'Registration start is required'),
    registrationEndDate: z.string().min(1, 'Registration end is required'),
    examWindowStartDate: z.string().min(1, 'Exam window start is required'),
    examWindowEndDate: z.string().min(1, 'Exam window end is required'),
    closureDate: z.string().min(1, 'Closure date is required'),
  })
  .refine((data) => new Date(data.registrationStartDate) < new Date(data.registrationEndDate), { message: 'Registration start must be before registration end', path: ['registrationEndDate'] })
  .refine((data) => new Date(data.registrationEndDate) < new Date(data.examWindowStartDate), { message: 'Registration must close before exam window starts', path: ['examWindowStartDate'] })
  .refine((data) => new Date(data.examWindowStartDate) < new Date(data.examWindowEndDate), { message: 'Exam start must be before exam end', path: ['examWindowEndDate'] })
  .refine((data) => new Date(data.examWindowEndDate) < new Date(data.closureDate), { message: 'Closure date must be after exam window', path: ['closureDate'] });

export const eligibilitySchema = z.object({
  prerequisiteCertification: z.string().optional(),
  managerApprovalRequired: z.boolean(),
  attemptLimit: z.coerce.number().min(1).max(5),
  tenureRequiredDays: z.coerce.number().min(0),
  minimumExperienceMonths: z.coerce.number().min(0),
  trainingCompletionRequired: z.boolean(),
  departmentRestrictions: z.string().optional(),
  businessUnitRestrictions: z.string().optional(),
  locationRestrictions: z.string().optional(),
});

export const voucherSchema = z.object({
  voucherVendor: z.string().min(1, 'Voucher vendor is required'),
  voucherValue: z.coerce.number().positive('Voucher value must be positive'),
  voucherQuantity: z.coerce.number().int().positive('Voucher quantity is required'),
  voucherExpiry: z.string().min(1, 'Voucher expiry is required'),
  voucherDistributionStrategy: z.string().min(1, 'Distribution strategy is required'),
});

export const budgetSchema = z
  .object({
    totalBudget: z.coerce.number().positive('Total budget must be positive'),
    trainingBudget: z.coerce.number().min(0),
    voucherBudget: z.coerce.number().min(0),
    contingencyBudget: z.coerce.number().min(0),
    approvalThreshold: z.coerce.number().min(0),
  })
  .refine((data) => data.trainingBudget + data.voucherBudget + data.contingencyBudget <= data.totalBudget, {
    message: 'Budget components cannot exceed total budget',
    path: ['totalBudget'],
  });

export const createDriveSchema = z
  .object({
    driveName: z.string().min(3, 'Drive name is required'),
    driveCode: z.string().min(3, 'Drive code is required'),
    vendorName: z.string().min(1, 'Vendor is required'),
    certificationName: z.string().min(1, 'Certification is required'),
    certificationLevel: z.string().min(1, 'Certification level is required'),
    sponsorName: z.string().min(1, 'Sponsor is required'),
    businessUnit: z.string().min(1, 'Business unit is required'),
    description: z.string().optional(),
    objectives: z.string().optional(),
  })
  .merge(timelineSchema)
  .merge(eligibilitySchema)
  .merge(voucherSchema)
  .merge(budgetSchema);
