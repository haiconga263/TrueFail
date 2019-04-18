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
    { label: 'Dashboard', route: 'dashboard', iconClasses: 'fa fa-dashboard' },
    { label: 'User', route: 'user', iconClasses: 'fa fa-user' },
    {label: 'Farmer', iconClasses: 'fa fa-pagelines', children: [
      { label: 'Farmer', route: 'farmer', iconClasses: 'fa fa-male' },
      { label: 'Planning', route: 'farmer/planning', iconClasses: 'fa fa-tasks' },
      { label: 'Planning history', route: 'farmer/planning-history', iconClasses: 'fa fa-history' },
      { label: 'Order', route: 'farmer/order', iconClasses: 'fa fa-shopping-bag' },
      { label: 'Order history', route: 'farmer/order-history', iconClasses: 'fa fa-history' }
    ]},
    {label: 'Retailer', iconClasses: 'fa fa-university', children: [
      { label: 'Retailer', route: 'retailer', iconClasses: 'fa fa-user-secret' },
      { label: 'Planning', route: 'retailer/planning', iconClasses: 'fa fa-balance-scale' },
      { label: 'Planning history', route: 'retailer/planning-history', iconClasses: 'fa fa-history' },
      { label: 'Order', route: 'retailer/order', iconClasses: 'fa fa-shopping-basket' },
      { label: 'Order history', route: 'retailer/order-history', iconClasses: 'fa fa-history' },
      { label: 'Order temp', route: 'retailer/order-temp', iconClasses: 'fa fa-shopping-basket' } 
    ]},
    {label: 'Master Data', iconClasses: 'fa fa-database', children: [
      { label: 'Product', route: 'product', iconClasses: 'fa fa-cubes' },
      { label: 'Category', route: 'category', iconClasses: 'fa fa-cubes' },
      { label: 'Employee', route: 'employee', iconClasses: 'fa fa-street-view' },
      { label: 'Collection', route: 'collection', iconClasses: 'fa fa-briefcase ' },
      { label: 'Fulfillment', route: 'fulfillment', iconClasses: 'fa fa-briefcase ' },
      { label: 'Distribution', route: 'distribution', iconClasses: 'fa fa-building  ' },
      {label: 'Geo', iconClasses: 'fa fa-flag', children: [
        { label: 'Country', route: 'geo/country', iconClasses: 'fa fa-crosshairs' },
        { label: 'Region', route: 'geo/region', iconClasses: 'fa fa-crosshairs' },
        { label: 'Province', route: 'geo/province', iconClasses: 'fa fa-crosshairs' },
        { label: 'District', route: 'geo/district', iconClasses: 'fa fa-crosshairs' },
        { label: 'Ward', route: 'geo/ward', iconClasses: 'fa fa-crosshairs' },
      ]},
      { label: 'Language', route: 'caption', iconClasses: 'fa fa-language ' },
      { label: 'Vehicle', route: 'vehicle', iconClasses: 'fa fa-truck ' },
    ]},
    { label: 'OTHER NAVIGATION', separator: true },
  ]
};
