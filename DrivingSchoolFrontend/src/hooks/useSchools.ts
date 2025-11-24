import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { schoolService, CreateSchoolDto, UpdateSchoolDto } from '@/services/schoolService';

export const useSchools = () => {
  return useQuery({
    queryKey: ['schools'],
    queryFn: schoolService.getAll,
  });
};

export const useSchool = (id: number) => {
  return useQuery({
    queryKey: ['schools', id],
    queryFn: () => schoolService.getById(id),
    enabled: !!id,
  });
};

export const useCreateSchool = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: schoolService.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['schools'] });
    },
  });
};

export const useUpdateSchool = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: schoolService.update,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['schools'] });
    },
  });
};

export const useDeleteSchool = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: schoolService.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['schools'] });
    },
  });
};
