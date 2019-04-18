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
    { label: 'Bản đồ', route: 'map', iconClasses: 'fa fa-map' },
    { label: 'Điều phối', route: 'trips', iconClasses: 'fa fa-desktop' },
    { label: 'Quản lý tuyến', route: 'router', iconClasses: 'fa fa-road' },
    {label: 'Báo cáo', iconClasses: 'fa fa-book', children: [
      { label: 'Lịch sử chuyến hàng', route: 'reports/trip-history', iconClasses: 'fa fa-truck' },
      { label: 'Lịch sử giao hàng', route: 'reports/order-history', iconClasses: 'fa fa-shopping-cart' }
    ]},
    { label: 'OTHER NAVIGATION', separator: true },
  ]
};
