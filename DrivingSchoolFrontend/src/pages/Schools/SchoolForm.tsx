import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import * as z from 'zod';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from '@/components/ui/dialog';
import { useCreateSchool, useUpdateSchool } from '@/hooks/useSchools';
import { School } from '@/services/schoolService';
import { useEffect } from 'react';
import { toast } from 'sonner';

const schoolSchema = z.object({
  schoolName: z.string().min(2, "Name must be at least 2 characters"),
  location: z.string().optional(),
  totalLectureHalls: z.coerce.number().min(0),
  seatsPerHall: z.coerce.number().min(0),
  startTime: z.string(),
  endTime: z.string(),
});

type SchoolFormValues = z.infer<typeof schoolSchema>;

interface SchoolFormProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  schoolToEdit?: School | null;
}

export default function SchoolForm({ open, onOpenChange, schoolToEdit }: SchoolFormProps) {
  const createSchool = useCreateSchool();
  const updateSchool = useUpdateSchool();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<SchoolFormValues>({
    resolver: zodResolver(schoolSchema),
    defaultValues: {
      schoolName: '',
      location: '',
      totalLectureHalls: 0,
      seatsPerHall: 0,
      startTime: '08:00',
      endTime: '17:00',
    },
  });

  useEffect(() => {
    if (schoolToEdit) {
      let st = '08:00';
      let et = '17:00';
      try {
        st = new Date(schoolToEdit.startTime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false });
        et = new Date(schoolToEdit.endTime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false });
      } catch (e) { 
        console.error("Error parsing time", e);
      }

      reset({
        schoolName: schoolToEdit.schoolName,
        location: schoolToEdit.location || '',
        totalLectureHalls: schoolToEdit.totalLectureHalls,
        seatsPerHall: schoolToEdit.seatsPerHall,
        startTime: st,
        endTime: et,
      });
    } else {
      reset({
        schoolName: '',
        location: '',
        totalLectureHalls: 0,
        seatsPerHall: 0,
        startTime: '08:00',
        endTime: '17:00',
      });
    }
  }, [schoolToEdit, reset, open]);

  const onSubmit = (data: SchoolFormValues) => {
    const today = new Date().toISOString().split('T')[0];
    const startDateTime = new Date(`${today}T${data.startTime}:00`).toISOString();
    const endDateTime = new Date(`${today}T${data.endTime}:00`).toISOString();

    const payload = {
        ...data,
        startTime: startDateTime,
        endTime: endDateTime,
    };

    if (schoolToEdit) {
      updateSchool.mutate({ id: schoolToEdit.schoolId, ...payload }, {
        onSuccess: () => {
          toast.success("School updated successfully");
          onOpenChange(false);
        },
        onError: () => toast.error("Failed to update school")
      });
    } else {
      createSchool.mutate(payload, {
        onSuccess: () => {
          toast.success("School created successfully");
          onOpenChange(false);
        },
        onError: () => toast.error("Failed to create school")
      });
    }
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>{schoolToEdit ? 'Edit School' : 'Add New School'}</DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="schoolName">School Name</Label>
            <Input id="schoolName" {...register("schoolName")} />
            {errors.schoolName && <p className="text-xs text-destructive">{errors.schoolName.message}</p>}
          </div>
          
          <div className="space-y-2">
            <Label htmlFor="location">Location</Label>
            <Input id="location" {...register("location")} />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
                <Label htmlFor="totalLectureHalls">Lecture Halls</Label>
                <Input type="number" id="totalLectureHalls" {...register("totalLectureHalls")} />
                {errors.totalLectureHalls && <p className="text-xs text-destructive">{errors.totalLectureHalls.message}</p>}
            </div>
            <div className="space-y-2">
                <Label htmlFor="seatsPerHall">Seats / Hall</Label>
                <Input type="number" id="seatsPerHall" {...register("seatsPerHall")} />
                {errors.seatsPerHall && <p className="text-xs text-destructive">{errors.seatsPerHall.message}</p>}
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
                <Label htmlFor="startTime">Start Time</Label>
                <Input type="time" id="startTime" {...register("startTime")} />
            </div>
            <div className="space-y-2">
                <Label htmlFor="endTime">End Time</Label>
                <Input type="time" id="endTime" {...register("endTime")} />
            </div>
          </div>

          <DialogFooter>
            <Button type="submit" disabled={createSchool.isPending || updateSchool.isPending}>
              {createSchool.isPending || updateSchool.isPending ? 'Saving...' : 'Save'}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
