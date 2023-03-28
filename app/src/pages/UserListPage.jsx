import ParentUserTable from '../ParentUserTable.jsx';

export default function UserListPage({ onUserSelected }) {
  return (
    <div>
      <ParentUserTable // return main parent users table
        onUserSelected={onUserSelected}
          ></ParentUserTable>
    </div>
  );
}
