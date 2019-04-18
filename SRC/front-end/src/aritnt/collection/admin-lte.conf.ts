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
    { label: 'Nhận hàng', route: 'receiving', iconClasses: 'fa fa-desktop' },
    { label: 'Chuyển hàng', route: 'shipping', iconClasses: 'fa fa-truck' },
    { label: 'Kho', route: 'inventory', iconClasses: 'fa fa-home' },
    { label: 'Báo cáo', iconClasses: 'fa fa-book', children: [
      { label: 'Lịch sử nhận hàng', route: 'reports/order-history', iconClasses: 'fa fa-truck' },
      { label: 'Lịch sử nhân viên', route: 'reports/employee-history', iconClasses: 'fa fa-shopping-cart' },
      { label: 'Lịch sử nhập xuất kho', route: 'reports/inventory-history', iconClasses: 'fa fa-building' },
      { label: 'Lịch sử xuất hàng', route: 'reports/shipping-history', iconClasses: 'fa fa-ship' }
    ]},
    { label: 'OTHER NAVIGATION', separator: true },
  ]
};
