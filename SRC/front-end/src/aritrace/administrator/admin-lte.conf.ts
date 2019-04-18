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
    { label: 'Companies', route: 'companies', iconClasses: 'fa fa-id-card-o' },
    {
      label: 'Catalog', iconClasses: 'fa fa-sitemap', children: [
        { label: 'Products', route: 'products', iconClasses: 'fa fa-cubes', },
        { label: 'Categories', route: 'categories', iconClasses: 'fa fa-tasks', },
        { label: 'Company Types', route: 'company-types', iconClasses: 'fa fa-tasks', },
        { label: 'Growing Methods', route: 'growing-methods', iconClasses: 'fa fa-tasks', },
        { label: 'Culture Fields', route: 'culture-fields', iconClasses: 'fa fa-th-large', },
      ]
    },
    {
      label: 'Users', iconClasses: 'fa fa-users', children: [
        { label: 'Accounts', route: 'accounts', iconClasses: 'fa fa-user' },
        { label: 'Roles', route: 'roles', iconClasses: 'fa fa-tasks' },
      ]
    },
    { label: 'CONFIGRATION', separator: true },
    { label: 'Units', route: 'uoms', iconClasses: 'fa fa-cubes' },
    {
      label: 'Configuration', iconClasses: 'fa fa-cog', children: [
        {
          label: 'Regional Settings', iconClasses: 'fa fa-map-marker', children: [
            { label: 'Countries', route: 'countries', iconClasses: 'fa fa-eercast' },
            { label: 'Regions', route: 'regions', iconClasses: 'fa fa-eercast' },
            { label: 'Provinces', route: 'provinces', iconClasses: 'fa fa-eercast' },
            { label: 'Districts', route: 'districts', iconClasses: 'fa fa-eercast' },
            { label: 'Wards', route: 'wards', iconClasses: 'fa fa-eercast' },
          ]
        },
        {
          label: 'Language Settings', iconClasses: 'fa fa-language', children: [
            { label: 'Languages', route: 'languages', iconClasses: 'fa fa-eercast' },
            { label: 'Captions', route: 'captions', iconClasses: 'fa fa-eercast' },
          ]
        },
        { label: 'Settings', route: 'settings', iconClasses: 'fa fa-sliders' },
      ]
    },
    { label: 'GLN', route: 'glns', iconClasses: 'fa fa-sliders' },
  ]
};
