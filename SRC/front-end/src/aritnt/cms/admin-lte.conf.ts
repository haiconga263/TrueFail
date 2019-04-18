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
    { label: 'Page', route: 'page', iconClasses: 'fa fa-file' },
    {
      label: 'Blog', iconClasses: 'fa fa-sitemap', children: [
        { label: 'Topic', route: 'topic', iconClasses: 'fa fa-cubes', },
        { label: 'Post', route: 'post', iconClasses: 'fa fa-tasks', },
      ]
    },
    { label: "FAQ's", route: 'faq', iconClasses: 'fa fa-comment' },
    { label: 'Media', route: 'image', iconClasses: 'fa fa-image' },
    { label: 'Contact', route: 'contact', iconClasses: 'fa fa-envelope' },
    { label: 'OTHER NAVIGATION', separator: true },
  ]
};
