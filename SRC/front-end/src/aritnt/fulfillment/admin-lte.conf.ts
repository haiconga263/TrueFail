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
    { label: 'Dashboard', route: 'dashboard', iconClasses: 'fa fa-cog' },
    { label: 'Nhận hàng', route: 'ful-collection', iconClasses: 'fa fa-cart-arrow-down' },
    
    // { label: 'Quy trình xử lý hàng', iconClasses: 'fa fa-filter', children: [
    //   { label: 'Lấy mẫu', route: 'reports/get-model', iconClasses: 'fa fa-get-pocket' },
    //   { label: 'Đánh giá', route: 'reports/rate-model', iconClasses: 'fa fa-check-square-o' },
    //   { label: 'Gắn code', route: 'reports/set-code-model', iconClasses: 'fa fa-barcode' }
    // ]},
    { label: 'Đóng gói', route: 'ful-pack', iconClasses: 'fa fa-inbox' },
    { label: 'Xuất kho', route: 'shipping', iconClasses: 'fa fa-cart-arrow-down' },
    { label: 'Quản lý team', route: 'team', iconClasses: 'fa fa-cart-arrow-down' },
    // { label: 'Hàng trả lại', route: 'supervision3', iconClasses: 'fa fa-cart-arrow-down' },
    // { label: 'Quản lý bao bì/ thùng hàng', route: 'supervision4', iconClasses: 'fa fa-cart-arrow-down' },
    
    { label: 'OTHER NAVIGATION', separator: true },
  ]
};
