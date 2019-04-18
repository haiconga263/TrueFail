export const adminLteConf = {
  skin: 'blue',
  // isSidebarLeftCollapsed: false,
  // isSidebarLeftExpandOnOver: false,
  // isSidebarLeftMouseOver: false,
  // isSidebarLeftMini: true,
  // sidebarRightSkin: 'dark',
  // isSidebarRightCollapsed: true,
  // isSidebarRightOverContent: true,
  // layout: 'normal',
  sidebarLeftMenu: [
    { label: 'MAIN NAVIGATION', separator: true },
    { label: 'Dashboard', route: 'dashboard', iconClasses: 'fa fa-area-chart' },
    { label: 'Production', route: 'productions', iconClasses: 'fa fa-cube' },
    { label: 'Process', route: 'processes', iconClasses: 'fa fa-cubes' },
    { label: 'Materials', route: 'materials', iconClasses: 'fa fa-cubes' },
    {
      label: 'GS1', iconClasses: 'fa fa-tasks', children: [
        { label: 'Global Trade Item Number', route: '', iconClasses: 'fa fa-cube' },
        { label: 'Global Location Number', route: '', iconClasses: 'fa fa-map-marker' },
      ]
    },
    {
      label: 'Users', iconClasses: 'fa fa-users', children: [
        { label: 'Accounts', route: 'accounts', iconClasses: 'fa fa-user' },
      ]
    },
  ]
};
