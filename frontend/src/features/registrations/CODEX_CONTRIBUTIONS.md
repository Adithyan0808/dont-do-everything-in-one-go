# Codex Contributions — Registration Feature (Step 10)

This file lists the core files and artifacts that Codex created when it started Step 10 (registration feature scaffold). The assistant continued and finished the UI/workflow in this step — see REGISTRATION_STEP10_CHANGELOG.md for assistant actions.

Core files created by Codex (paths & short description):

- constants/registrationConstants.js — lifecycle statuses, journey stages, query keys, styles
- services/registrationMockData.js — demo drives, registrations, eligibility, timeline, communications, audit, approvals
- utils/RegistrationActionResolver.js — next-action resolver for cards/buttons
- utils/RegistrationGuidanceEngine.js — guidance text per status
- utils/registrationFormatters.js — small helpers/formatters
- api/registrationApi.js — feature API facade with withFallback() to mock data
- hooks/useRegistrationQueries.js — React Query hooks (register, my registrations, detail, timeline, eligibility, audit, communications, approvals)
- hooks/useRegistrations.js — re-export / index for hooks
- schemas/registrationSchemas.js — Zod schemas for wizard, exam prefs, approvals, lookup

Components & UI scaffold:

- components/RegistrationCard.jsx
- components/RegistrationJourneyTracker.jsx
- components/RegistrationStatusBadge.jsx
- components/RegistrationSkeleton.jsx
- components/RegistrationErrorState.jsx
- components/DetailWorkspaceSkeleton.jsx
- components/JourneyTrackerSkeleton.jsx
- components/ApprovalTableSkeleton.jsx

Widgets & Forms:

- widgets/EligibilityPreviewWidget.jsx
- forms/RegistrationWizard.jsx (multi-step enrollment wizard)

Pages:

- pages/RegisterDrivePage.jsx
- pages/RegisterForDrivePage.jsx

Notes:
- Many of these files are mock-driven so the UI runs without backend endpoints.
- Assistant has continued from this scaffold to implement full demo pages and documentation.
