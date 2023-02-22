import * as React from 'react';
import { useState } from 'react';
import UserListPage from './pages/UserListPage.jsx';
import UserPage from './pages/UserPage.jsx';

function App() {
  const [selectedUser, setSelectedUser] = useState(null);

  const handleUserSelected = (user) => {
    setSelectedUser(user);
  };

  const handleClearUser = () => {
    setSelectedUser(null);
  };

  const poorMansRouter = () => {
    if (selectedUser) {
      return <UserPage user={selectedUser} onClearUser={handleClearUser} />;
    } else {
      return <UserListPage onUserSelected={handleUserSelected} />;
    }
  };

  return <div className="App">{poorMansRouter()}</div>;
}

export default App;
