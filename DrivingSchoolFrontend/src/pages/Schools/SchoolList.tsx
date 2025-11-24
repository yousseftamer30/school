import { useState } from 'react';
import { useSchools, useDeleteSchool } from '@/hooks/useSchools';
import { School } from '@/services/schoolService';
import { DataTable } from '@/components/ui/data-table';
import { Button } from '@/components/ui/button';
import { Plus, Pencil, Trash2 } from 'lucide-react';
import { ColumnDef } from '@tanstack/react-table';
import SchoolForm from './SchoolForm';
import { toast } from 'sonner';

export default function SchoolList() {
  const { data: schools, isLoading } = useSchools();
  const deleteSchool = useDeleteSchool();
  const [isFormOpen, setIsFormOpen] = useState(false);
  const [editingSchool, setEditingSchool] = useState<School | null>(null);

  const handleDelete = (id: number) => {
    if (confirm("Are you sure you want to delete this school?")) {
      deleteSchool.mutate(id, {
        onSuccess: () => toast.success("School deleted successfully"),
        onError: () => toast.error("Failed to delete school")
      });
    }
  };

  const columns: ColumnDef<School>[] = [
    {
      accessorKey: 'schoolName',
      header: 'School Name',
    },
    {
      accessorKey: 'location',
      header: 'Location',
    },
    {
      accessorKey: 'totalLectureHalls',
      header: 'Halls',
    },
    {
      accessorKey: 'seatsPerHall',
      header: 'Seats/Hall',
    },
    {
      id: 'actions',
      cell: ({ row }) => {
        const school = row.original;
        return (
          <div className="flex gap-2 justify-end">
            <Button variant="ghost" size="icon" onClick={() => {
              setEditingSchool(school);
              setIsFormOpen(true);
            }}>
              <Pencil className="h-4 w-4" />
            </Button>
            <Button variant="ghost" size="icon" className="text-destructive hover:text-destructive" onClick={() => handleDelete(school.schoolId)}>
              <Trash2 className="h-4 w-4" />
            </Button>
          </div>
        );
      },
    },
  ];

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold">Schools</h1>
        <Button onClick={() => {
          setEditingSchool(null);
          setIsFormOpen(true);
        }}>
          <Plus className="mr-2 h-4 w-4" /> Add School
        </Button>
      </div>

      <DataTable columns={columns} data={schools || []} isLoading={isLoading} />

      <SchoolForm 
        open={isFormOpen} 
        onOpenChange={setIsFormOpen} 
        schoolToEdit={editingSchool} 
      />
    </div>
  );
}
