import { Users, CalendarDays, CreditCard, CheckCircle2 } from 'lucide-react';

export default function Dashboard() {
  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-3xl font-bold tracking-tight">Dashboard</h1>
        <p className="text-muted-foreground">Overview of driving school operations.</p>
      </div>
      
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
        <div className="p-6 bg-card rounded-lg border shadow-sm">
          <div className="flex items-center justify-between space-y-0 pb-2">
            <h3 className="tracking-tight text-sm font-medium text-muted-foreground">Total Students</h3>
            <Users className="h-4 w-4 text-muted-foreground" />
          </div>
          <div className="mt-2">
            <div className="text-2xl font-bold">1,234</div>
            <p className="text-xs text-muted-foreground">+180 from last month</p>
          </div>
        </div>

        <div className="p-6 bg-card rounded-lg border shadow-sm">
          <div className="flex items-center justify-between space-y-0 pb-2">
            <h3 className="tracking-tight text-sm font-medium text-muted-foreground">Active Reservations</h3>
            <CalendarDays className="h-4 w-4 text-muted-foreground" />
          </div>
          <div className="mt-2">
            <div className="text-2xl font-bold">573</div>
            <p className="text-xs text-muted-foreground">+201 since last hour</p>
          </div>
        </div>

        <div className="p-6 bg-card rounded-lg border shadow-sm">
          <div className="flex items-center justify-between space-y-0 pb-2">
            <h3 className="tracking-tight text-sm font-medium text-muted-foreground">Revenue (Month)</h3>
            <CreditCard className="h-4 w-4 text-muted-foreground" />
          </div>
          <div className="mt-2">
            <div className="text-2xl font-bold">$45,231.89</div>
            <p className="text-xs text-muted-foreground">+19% from last month</p>
          </div>
        </div>

        <div className="p-6 bg-card rounded-lg border shadow-sm">
          <div className="flex items-center justify-between space-y-0 pb-2">
            <h3 className="tracking-tight text-sm font-medium text-muted-foreground">Pass Rate</h3>
            <CheckCircle2 className="h-4 w-4 text-muted-foreground" />
          </div>
          <div className="mt-2">
            <div className="text-2xl font-bold">89.5%</div>
            <p className="text-xs text-muted-foreground">+2.1% from last month</p>
          </div>
        </div>
      </div>
    </div>
  )
}
