import api from './api';

export interface School {
  schoolId: number;
  schoolName: string;
  govId: number;
  location?: string;
  startTime: string;
  endTime: string;
  totalLectureHalls: number;
  seatsPerHall: number;
  totalCapacity?: number;
}

export interface CreateSchoolDto {
  schoolName: string;
  location?: string;
  totalLectureHalls: number;
  seatsPerHall: number;
  startTime: string;
  endTime: string;
}

export interface UpdateSchoolDto extends CreateSchoolDto {
  id: number;
}

export const schoolService = {
  getAll: async () => {
    const response = await api.get<{ success: boolean; data: School[] }>('/schools');
    return response.data.data;
  },
  getById: async (id: number) => {
    const response = await api.get<{ success: boolean; data: School }>(`/schools/${id}`);
    return response.data.data;
  },
  create: async (data: CreateSchoolDto) => {
    const response = await api.post<{ success: boolean; data: School }>('/schools', data);
    return response.data.data;
  },
  update: async (data: UpdateSchoolDto) => {
    const response = await api.put<{ success: boolean }>(`/schools/${data.id}`, data);
    return response.data;
  },
  delete: async (id: number) => {
    const response = await api.delete<{ success: boolean }>(`/schools/${id}`);
    return response.data;
  },
};
