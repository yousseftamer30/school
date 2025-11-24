import { LogOut, User } from 'lucide-react';

export default function TopBar() {
  // TODO: Get user info from store
  const user = { name: "Admin User" };

  const handleLogout = () => {
    console.log("Logout");
    // TODO: Implement logout
  };

  return (
    <header className="flex items-center justify-between px-6 py-4 border-b bg-card">
      <div className="flex items-center">
         {/* Breadcrumbs placeholder */}
         <h2 className="text-lg font-medium">Dashboard</h2>
      </div>
      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2">
          <div className="p-1 bg-secondary rounded-full">
            <User className="w-5 h-5" />
          </div>
          <span className="text-sm font-medium">{user.name}</span>
        </div>
        <button onClick={handleLogout} className="p-2 hover:bg-accent rounded-full text-muted-foreground hover:text-foreground transition-colors" aria-label="Logout">
          <LogOut className="w-5 h-5" />
        </button>
      </div>
    </header>
  );
}
