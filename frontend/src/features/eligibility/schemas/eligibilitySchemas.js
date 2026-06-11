import { z } from 'zod';

export const approvalDecisionSchema = z.object({
  approver: z.string().min(1, 'Approver is required'),
  comments: z.string().optional(),
});

export const rejectionSchema = z.object({
  reason: z.enum(['Insufficient Tenure', 'Training Incomplete', 'Exceeded Attempts', 'Budget Unavailable', 'Policy Restriction', 'Other']),
  comments: z.string().min(2, 'Provide comments'),
});

export const bulkApprovalSchema = z.object({
  ids: z.array(z.string()).min(1, 'Select at least one registration'),
});
