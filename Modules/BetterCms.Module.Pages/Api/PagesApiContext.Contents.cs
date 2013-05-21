﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using NHibernate.Linq;

using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.DataContracts.Enums;
using BetterCms.Core.Exceptions.Api;

using BetterCms.Module.Pages.Api.Dto;
using BetterCms.Module.Pages.Helpers;
using BetterCms.Module.Pages.Models;
using BetterCms.Module.Root.Models;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;
using BetterCms.Module.Root.Services;

// ReSharper disable CheckNamespace
namespace BetterCms.Api
// ReSharper restore CheckNamespace
{
    public partial class PagesApiContext
    {
        /// <summary>
        /// Gets the list of page content entities.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// Page content entities list
        /// </returns>
        public IList<PageContent> GetPageContents(Guid pageId, Expression<Func<PageContent, bool>> filter = null)
        {
            try
            {
                return Repository
                    .AsQueryable<PageContent>()
                    .Where(p => p.Page.Id == pageId)
                    .ApplyFilters(filter, p => p.Order)
                    .Fetch(c => c.Content)
                    .Fetch(c => c.Region)
                    .FetchMany(c => c.Options)
                    .ToList();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get page contents by page id {0}.", pageId);
                Logger.Error(message, inner);

                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the list of page region contents.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// Page content entities list
        /// </returns>
        public IList<PageContent> GetRegionContents(Guid pageId, Guid regionId, Expression<Func<PageContent, bool>> filter = null)
        {
            try
            {
                return Repository
                    .AsQueryable<PageContent>()
                    .Where(p => p.Page.Id == pageId && p.Region.Id == regionId)
                    .ApplyFilters(filter, p => p.Order)
                    .Fetch(c => c.Content)
                    .Fetch(c => c.Region)
                    .FetchMany(c => c.Options)
                    .ToList();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get page region contents by page id={0} and region id={1}.", pageId, regionId);
                Logger.Error(message, inner);

                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the list of page region contents.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="regionIdentifier">The region identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// Page content entities list
        /// </returns>
        public IList<PageContent> GetRegionContents(Guid pageId, string regionIdentifier, Expression<Func<PageContent, bool>> filter = null)
        {
            try
            {
                return Repository
                    .AsQueryable<PageContent>()
                    .Where(p => p.Page.Id == pageId && p.Region.RegionIdentifier == regionIdentifier)
                    .ApplyFilters(filter, p => p.Order)
                    .Fetch(c => c.Content)
                    .Fetch(c => c.Region)
                    .FetchMany(c => c.Options)
                    .ToList();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get page region contents by page id={0} and region identifier={1}.", pageId, regionIdentifier);
                Logger.Error(message, inner);

                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the content entity.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// Content entity
        /// </returns>
        public Content GetContent(Guid id)
        {
            try
            {
                return Repository.First<Content>(id);
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get content by id={0}.", id);
                Logger.Error(message, inner);

                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the content of the page entity.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// Page content entity
        /// </returns>
        public PageContent GetPageContent(Guid id)
        {
            try
            {
                return Repository
                    .AsQueryable<PageContent>()
                    .Where(c => c.Id == id)
                    .Fetch(c => c.Content)
                    .Fetch(c => c.Region)
                    .FirstOne();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get page content by id={0}.", id);
                Logger.Error(message, inner);

                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the HTML content widget.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Html widget.</returns>
        /// <exception cref="CmsApiException">Failed to get html content widget.</exception>
        public HtmlContentWidget GetHtmlContentWidget(Guid id)
        {
            try
            {
                return Repository.First<HtmlContentWidget>(id);
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get html content widget by id={0}.", id);
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the server control widget.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Server widget.</returns>
        /// <exception cref="CmsApiException">Failed to get server control widget.</exception>
        public ServerControlWidget GetServerControlWidget(Guid id)
        {
            try
            {
                return Repository.First<ServerControlWidget>(id);
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get server control widget by id={0}.", id);
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the widgets.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="order">The order.</param>
        /// <param name="orderDescending">if set to <c>true</c> [order descending].</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <returns>Widget list.</returns>
        /// <exception cref="CmsApiException">Failed to get widgets.</exception>
        public IList<Widget> GetWidgets(Expression<Func<Widget, bool>> filter = null, Expression<Func<Widget, dynamic>> order = null, bool orderDescending = false, int? pageNumber = null, int? itemsPerPage = null)
        {
            try
            {
                if (order == null)
                {
                    order = p => p.Name;
                }

                return Repository.AsQueryable(filter, order, orderDescending, pageNumber, itemsPerPage).ToList();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get widgets.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the page widgets.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>Widget list.</returns>
        /// <exception cref="CmsApiException">Failed to get widgets.</exception>
        public IList<Widget> GetPageWidgets(Guid pageId, Expression<Func<Widget, bool>> filter = null)
        {
            try
            {
                var query = Repository
                    .AsQueryable<Widget>()
                    .Where(w => w.PageContents != null && w.PageContents.Any(c => c.Page.Id == pageId));

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return query.ToList();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get page widgets.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Gets the list with historical content entities.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="order">The order.</param>
        /// <param name="orderDescending">if set to <c>true</c> order by descending.</param>
        /// <returns>
        /// Historical content entities
        /// </returns>
        public IList<Content> GetContentHistory(Guid contentId, Expression<Func<Content, bool>> filter = null, Expression<Func<Content, dynamic>> order = null, bool orderDescending = false)
        {
            try
            {
                return historyService.GetContentHistory(contentId, new SearchableGridOptions())
                    .AsQueryable()
                    .ApplyFilters(filter, order, orderDescending)
                    .ToList();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to get history for content id={0}.", contentId);
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Creates the HTML content widget.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Created widget entity
        /// </returns>
        /// <exception cref="CmsApiException"></exception>
        /// <exception cref="BetterCms.Core.Exceptions.Api.CmsApiException"></exception>
        public HtmlContentWidget CreateHtmlContentWidget(CreateHtmlContentWidgetRequest request)
        {
            try
            {
                UnitOfWork.BeginTransaction();

                var htmlWidget = new HtmlContentWidget
                {
                    Name = request.Name,
                    Html = request.Html,
                    CustomCss = request.Css,
                    CustomJs = request.JavaScript,
                    UseHtml = !string.IsNullOrWhiteSpace(request.Html),
                    UseCustomCss = !string.IsNullOrWhiteSpace(request.Css),
                    UseCustomJs = !string.IsNullOrWhiteSpace(request.JavaScript)
                };

                if (request.CategoryId.HasValue && !request.CategoryId.Value.HasDefaultValue())
                {
                    htmlWidget.Category = Repository.AsProxy<Category>(request.CategoryId.Value);
                }

                var service = Resolve<IContentService>();
                var widgetToSave = (HtmlContentWidget)service.SaveContentWithStatusUpdate(htmlWidget, ContentStatus.Published);

                Repository.Save(widgetToSave);
                UnitOfWork.Commit();

                // Notify
                Events.OnWidgetCreated(widgetToSave);

                return widgetToSave;
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to create HTML content widget.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Creates the server control widget.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Created widget entity
        /// </returns>
        /// <exception cref="CmsApiValidationException"></exception>
        /// <exception cref="CmsApiException"></exception>
        /// <exception cref="BetterCms.Core.Exceptions.Api.CmsApiException"></exception>
        public ServerControlWidget CreateServerControlWidget(CreateServerControlWidgetRequest request)
        {
            if (!HttpHelper.VirtualPathExists(request.WidgetPath))
            {
                var message = string.Format("Failed to create server control widget: view by given path {0} doesn't exist.", request.WidgetPath);
                Logger.Error(message);
                throw new CmsApiValidationException(message);
            }

            try
            {
                UnitOfWork.BeginTransaction();

                var serverWidget = new ServerControlWidget
                {
                    Name = request.Name,
                    Url = request.WidgetPath,
                    PreviewUrl = request.PreviewUrl
                };

                if (request.CategoryId.HasValue && !request.CategoryId.Value.HasDefaultValue())
                {
                    serverWidget.Category = Repository.AsProxy<Category>(request.CategoryId.Value);
                }

                var service = Resolve<IContentService>();
                var widgetToSave = (ServerControlWidget)service.SaveContentWithStatusUpdate(serverWidget, ContentStatus.Published);

                Repository.Save(widgetToSave);

                if (request.Options != null && request.Options.Count > 0)
                {
                    SaveServerWidgetOptions(widgetToSave, request.Options);
                }

                UnitOfWork.Commit();

                // Notify
                Events.OnWidgetCreated(widgetToSave);

                return widgetToSave;
            }
            catch (CmsApiValidationException inner)
            {
                var message = string.Format("Failed to create server control widget.");
                Logger.Error(message, inner);
                throw;
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to create server control widget.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Creates the server widget options.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List with saved content option entities</returns>
        public IList<ContentOption> CreateServerWidgetOptions(CreateContentOptionsRequest request)
        {
            try
            {
                UnitOfWork.BeginTransaction();

                var widget = Repository.AsProxy<ServerControlWidget>(request.ContentId);
                var options = SaveServerWidgetOptions(widget, request.Options);

                UnitOfWork.Commit();

                return options;
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to create server content options.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Creates the content of the page HTML.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Page content entity with created HTML content
        /// </returns>
        public PageContent CreatePageHtmlContent(CreatePageHtmlContentRequest request)
        {
            var region = GetRegion(request);

            try
            {
                UnitOfWork.BeginTransaction();

                var content = new HtmlContent
                {
                    Name = request.Name,
                    ActivationDate = request.ActivationDate ?? DateTime.Today,
                    ExpirationDate = TimeHelper.FormatEndDate(request.ExpirationDate),
                    Html = request.Html ?? string.Empty,
                    CustomCss = request.CustomCss,
                    UseCustomCss = !string.IsNullOrWhiteSpace(request.CustomCss),
                    CustomJs = request.CustomJs,
                    UseCustomJs = !string.IsNullOrWhiteSpace(request.CustomJs)
                };

                var contentService = Resolve<IContentService>();
                var contentToSave = contentService.SaveContentWithStatusUpdate(content, request.ContentStatus);

                var pageContent = SavePageContent(request.PageId, region, contentToSave);

                UnitOfWork.Commit();

                return pageContent;
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to create page HTML content.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Adds the HTML content widget to page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Created page content entity containing HTML widget entity
        /// </returns>
        public PageContent AddHtmlContentWidgetToPage(AddWidgetToPageRequest request)
        {
            var region = GetRegion(request);

            try
            {
                UnitOfWork.BeginTransaction();

                var content = Repository.AsQueryable<HtmlContentWidget>(w => w.Id == request.ContentId).FirstOne();
                var pageContent = SavePageContent(request.PageId, region, content);

                UnitOfWork.Commit();

                return pageContent;
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to add HTML widget to page.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Adds the server control widget to page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Created page content entity containing server control widget entity
        /// </returns>
        public PageContent AddServerControlWidgetToPage(AddWidgetToPageRequest request)
        {
            var region = GetRegion(request);

            try
            {
                UnitOfWork.BeginTransaction();

                var content = Repository.AsQueryable<ServerControlWidget>(w => w.Id == request.ContentId).FirstOne();
                var pageContent = SavePageContent(request.PageId, region, content);

                UnitOfWork.Commit();

                return pageContent;
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to add server control widget to page.");
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Saves the content of the page.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="region">The region.</param>
        /// <param name="content">The content.</param>
        /// <returns>Saved page content entity containing content entity</returns>
        private PageContent SavePageContent(Guid pageId, Region region, Content content)
        {
            var contentService = Resolve<IContentService>();

            var page = Repository.AsProxy<Page>(pageId);

            var pageContent = new PageContent
            {
                Content = content,
                Order = contentService.GetPageContentNextOrderNumber(pageId),
                Page = page,
                Region = region
            };

            Repository.Save(pageContent);

            Events.OnPageContentInserted(pageContent);

            return pageContent;
        }

        /// <summary>
        /// Gets the region by RegionId or by RegionIdentifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Loaded region entity or region entity proxy</returns>
        private Region GetRegion(CreatePageContentRequestBase request)
        {
            if ((!request.RegionId.HasValue || request.RegionId.Value.HasDefaultValue()) 
                && string.IsNullOrWhiteSpace(request.RegionIdentifier))
            {
                var message = "Either region id or region identifier must be set.";
                Logger.Error(message);
                throw new CmsApiException(message);
            }

            if (request.RegionId.HasValue)
            {
                return Repository.AsProxy<Region>(request.RegionId.Value);
            }

            try
            {
                return Repository.AsQueryable<Region>(r => r.RegionIdentifier == request.RegionIdentifier).FirstOne();
            }
            catch (Exception inner)
            {
                var message = string.Format("Failed to load region by identifier: {0}.", request.RegionIdentifier);
                Logger.Error(message, inner);
                throw new CmsApiException(message, inner);
            }
        }

        /// <summary>
        /// Saves the server widget options.
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <param name="requestOptions">The request options.</param>
        /// <returns> List of saved widget options </returns>
        private IList<ContentOption> SaveServerWidgetOptions(ServerControlWidget widget, IList<ContentOptionDto> requestOptions)
        {
            var options = new List<ContentOption>();

            if (requestOptions != null && requestOptions.Count > 0)
            {
                foreach (var requestOption in requestOptions)
                {
                    var option = new ContentOption { Key = requestOption.Key, DefaultValue = requestOption.DefaultValue, Type = requestOption.Type, Content = widget };

                    options.Add(option);
                    Repository.Save(option);
                }
            }

            return options;
        }
    }
}