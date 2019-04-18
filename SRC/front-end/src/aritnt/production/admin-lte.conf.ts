export const adminLteConf = {
  skin: 'green',
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
    { label: 'Dashboard', route: 'dashboard', iconClasses: 'fa fa-dashboard' },
    { label: 'Plot', route: 'plot', iconClasses: 'fa fa-dashboard' },
    { label: 'Cultivation', route: 'cultivation', iconClasses: 'fa fa-dashboard' },
    {
      label: 'Master Data', iconClasses: 'fa fa-database', children: [
    { label: 'Seed', route: 'seed', iconClasses: 'fa fa-dashboard' },
    { label: 'Cultivation Method', route: 'method', iconClasses: 'fa fa-dashboard' },
    {
          label: 'Fertilizer', route: 'fertilizer', iconClasses: 'fa fa-cubes', children: [
            { label: 'Category', route: 'fertilizer/category' },
          ]
        },
        {
          label: 'Pesticide', route: 'pesticide', iconClasses: 'fa fa-cubes', children: [
            { label: 'Category', route: 'pesticide/category' },
          ]
        },
      ]
    },
    { label: 'OTHER NAVIGATION', separator: true },
  ]
};
