import { NavLink } from 'react-router-dom';
import { 
  LayoutDashboard, 
  School, 
  FileBadge, 
  Users, 
  UserCog, 
  Car, 
  Settings, 
  CreditCard, 
  CalendarDays, 
  CalendarCheck,
  FileText,
  GraduationCap,
  MapPin,
  Cog
} from 'lucide-react';
import { cn } from '@/lib/utils';

const navItems = [
  {
    group: "Overview",
    items: [
      { name: "Dashboard", icon: LayoutDashboard, href: "/" }
    ]
  },
  {
    group: "Operations",
    items: [
      { name: "Customers", icon: Users, href: "/customers" },
      { name: "Reservations", icon: CalendarDays, href: "/reservations" },
      { name: "Payments", icon: CreditCard, href: "/payments" },
      { name: "Session Scheduling", icon: CalendarCheck, href: "/sessions" },
    ]
  },
  {
    group: "Lookups",
    items: [
      { name: "Schools", icon: School, href: "/schools" },
      { name: "License Types", icon: FileBadge, href: "/license-types" },
      { name: "License Groups", icon: FileBadge, href: "/license-groups" },
      { name: "Roles", icon: Settings, href: "/roles" },
      { name: "Employees", icon: UserCog, href: "/employees" },
      { name: "Vehicles", icon: Car, href: "/vehicles" },
      { name: "Traffic Units", icon: MapPin, href: "/traffic-units" },
      { name: "Transmission Types", icon: Cog, href: "/transmission-types" },
      { name: "Course Lists", icon: FileText, href: "/course-lists" },
    ]
  }
];

export default function Sidebar() {
  return (
    <aside className="w-64 bg-card border-r flex flex-col hidden md:flex h-full">
      <div className="p-6 border-b">
        <h1 className="text-xl font-bold flex items-center gap-2 text-primary">
          <GraduationCap className="w-6 h-6" />
          Driving School
        </h1>
      </div>
      <div className="flex-1 overflow-y-auto py-4">
        <nav className="px-4 space-y-6">
          {navItems.map((group) => (
            <div key={group.group}>
              <h3 className="mb-2 px-2 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                {group.group}
              </h3>
              <div className="space-y-1">
                {group.items.map((item) => (
                  <NavLink
                    key={item.name}
                    to={item.href}
                    className={({ isActive }) =>
                      cn(
                        "flex items-center gap-3 px-3 py-2 rounded-md text-sm font-medium transition-colors",
                        isActive 
                          ? "bg-primary text-primary-foreground" 
                          : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
                      )
                    }
                  >
                    <item.icon className="w-4 h-4" />
                    {item.name}
                  </NavLink>
                ))}
              </div>
            </div>
          ))}
        </nav>
      </div>
    </aside>
  );
}
