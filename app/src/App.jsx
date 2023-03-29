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
    if (selectedUser) { // if user selected
      return <UserPage user={selectedUser} onClearUser={handleClearUser} />; // return user page with that user passed to it and clear it 
    } else {
      return <UserListPage onUserSelected={handleUserSelected} />; // otherwise, user not selected, return the table 
    }
  };

  return <div className="App">{poorMansRouter()}</div>;
}



export default App;
