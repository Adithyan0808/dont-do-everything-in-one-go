import apiClient from './axios';

export const assessmentApi = {
  submit: (payload) => apiClient.post('/assessments/submit', payload),
  importFile: (file) => {
    const formData = new FormData();
    formData.append('file', file);
    return apiClient.post('/assessments/import', formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    });
  },
  getById: (assessmentId) => apiClient.get(`/assessments/${assessmentId}`),
  getByDrive: (driveId) => apiClient.get(`/assessments/drive/${driveId}`),
};
