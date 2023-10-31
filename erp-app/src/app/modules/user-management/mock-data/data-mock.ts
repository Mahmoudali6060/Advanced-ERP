import { Privileges } from "src/app/shared/enums/privileges.enum";

export const DATA = [
  {
    id: 1,
    name: 'UserManagement',
    checked: false,
    level: 0,
    children: [
      {
        id: 2,
        name: 'Users',
        checked: false,
        categoryId: 1,
        level: 1,
        children: [
          {
            id: Privileges.UserManagement.Users.View,
            name: 'View',
            checked: false,
            categoryId: 1,
            pageId: 2,
            level: 2,
            children: []
          },
          {
            id: Privileges.UserManagement.Users.Add,
            name: 'Add',
            checked: false,
            categoryId: 1,
            pageId: 2,
            level: 2,
            children: []
          },
          {
            id: Privileges.UserManagement.Users.Edit,
            name: 'Edit',
            checked: false,
            categoryId: 1,
            pageId: 2,
            level: 2,
            children: []

          },
          {
            id: Privileges.UserManagement.Users.Delete,
            name: 'Delete',
            checked: false,
            categoryId: 1,
            pageId: 2,
            level: 2,
            children: []
          }
        ]
      }
    ]
  }
]