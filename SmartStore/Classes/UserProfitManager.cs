using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Classes
{
    public class UserProfitManager
    {
        private readonly SmartStoreContext db;
        public UserProfitManager()
        {
            this.db = new SmartStoreContext();
        }
        public UserProfitManager(SmartStoreContext db)
        {
            this.db = db;
        }
        public List<UserPoint> GetUserPoints(int id)
        {
            var user = db.Users.Find(id);
            var userPoints = db.UserPoints.Where(u => u.UserId == user.Id).ToList();
            return userPoints;
        }
        public List<UserPoint> GetUserPoints(User model)
        {
            var user = model;
            var userPoints = db.UserPoints.Where(u => u.UserId == user.Id).ToList();
            return userPoints;
        }
        public float GetUserPointsAmount(int id)
        {
            var user = db.Users.Find(id);
            var userPoints = db.UserPoints.Where(u => u.UserId == user.Id).Select(u => u.Point).ToList().Sum();
            return userPoints;
        }
        public float GetUserPointsAmount(User model)
        {
            var user = model;
            var userPoints = db.UserPoints.Where(u => u.UserId == user.Id).Select(u => u.Point).ToList().Sum();
            return userPoints;
        }
        public List<User> GetUserSubsets(int id)
        {
            var user = db.Users.Find(id);
            return db.Users.Where(u => u.UserIdentifierCode == user.UserCode).ToList();
        }
        public List<User> GetUserSubsets(User model)
        {
            var user = model;
            return db.Users.Where(u => u.UserIdentifierCode == user.UserCode).ToList();
        }
        public List<UserPoint> GetUserSubsetPoints(int id)
        {
            var userSubsetPoints = new List<UserPoint>();
            var user = db.Users.Find(id);
            var userSubsets = GetUserSubsets(user.Id);
            foreach (var item in userSubsets)
            {
                userSubsetPoints.AddRange(GetUserPoints(item));
                userSubsetPoints.AddRange(GetUserSubsetPoints(item));
            }
            return userSubsetPoints;
        }
        public List<UserPoint> GetUserSubsetPoints(User model)
        {
            var userSubsetPoints = new List<UserPoint>();
            var user = model;
            var userSubsets = GetUserSubsets(user.Id);
            foreach (var item in userSubsets)
            {
                userSubsetPoints.AddRange(GetUserPoints(item));
                userSubsetPoints.AddRange(GetUserSubsetPoints(item));
            }
            return userSubsetPoints;
        }
        public float GetUserSubsetPointsAmount(int id)
        {
            float userSubsetPoints = 0;
            var user = db.Users.Find(id);
            var userSubsets = GetUserSubsets(user.Id);
            foreach (var item in userSubsets)
            {
                userSubsetPoints += GetUserPointsAmount(item);
                userSubsetPoints += GetUserSubsetPointsAmount(item);
            }
            return userSubsetPoints;
        }
        public float GetUserSubsetPointsAmount(User model)
        {
            float userSubsetPoints = 0;
            var user = model;
            var userSubsets = GetUserSubsets(user.Id);
            foreach (var item in userSubsets)
            {
                userSubsetPoints += GetUserPointsAmount(item);
                userSubsetPoints += GetUserSubsetPointsAmount(item);
            }
            return userSubsetPoints;
        }
        public List<UserProfitPayment> GetUserPayments(int id)
        {
            return db.UserProfitPayments.Where(u => u.UserId == id).OrderByDescending(u => u.Date).ToList();
        }
        public UserProfitPayment GetLatestUserPayment(int id)
        {
            var userPayments = GetUserPayments(id);
            if (userPayments == null || userPayments.Count == 0) return null;
            return userPayments.FirstOrDefault();
        }
        public long GetTotalUserPaymentAmount(int id)
        {
            long totalUserPaymentAmount = 0;
            var userPayments = GetUserPayments(id);

            foreach (var payment in userPayments)
                totalUserPaymentAmount += payment.Amount;

            return totalUserPaymentAmount;
        }
        public User GetUsersWeakerSubset(int id)
        {
            var user = db.Users.Find(id);
            var userSubsets = GetUserSubsets(user.Id);
            if (userSubsets.Any() == false) return null;
            return userSubsets.OrderBy(u => GetUserSubsetPoints(u)).FirstOrDefault();
        }
        public User GetUsersWeakerSubset(User model)
        {
            var user = model;
            var userSubsets = GetUserSubsets(user.Id);
            if (userSubsets.Any() == false) return null;
            float a = 0;
            var weakerSubset = new User();
            foreach (var item in userSubsets)
            {
                var subsetPoints = GetUserPointsAmount(item);
                var subsetChildrenPoints = GetUserSubsetPointsAmount(item);
                subsetPoints += subsetChildrenPoints;
                if (a == 0)
                {
                    a = subsetPoints;
                    weakerSubset = item;
                } 
                else if (subsetPoints < a)
                {
                    a = subsetPoints;
                    weakerSubset = item;
                }
            }
            return weakerSubset;
        }
        public long CalculateUserProfit(int id)
        {
            long userProfit = 0;
            var user = db.Users.Find(id);
            //var latestUserPayment = GetLatestUserPayment(user.Id);
            var totalUserPayment = GetTotalUserPaymentAmount(user.Id);
            var subsetWingWithLessPoints = GetUsersWeakerSubset(user);
            if (subsetWingWithLessPoints != null)
            {
                var subsetPoints = GetUserPoints(subsetWingWithLessPoints);
                var subsetChildrenPoints = GetUserSubsetPoints(subsetWingWithLessPoints);
                subsetPoints.AddRange(subsetChildrenPoints);

                //if (latestUserPayment != null)
                //    subsetPoints = subsetPoints.Where(u => u.RegisterDate > latestUserPayment.Date).ToList();

                // Maximum Profit Control
                subsetPoints = subsetPoints.OrderBy(u => u.RegisterDate).ToList();
                var oldestRegisteredPoint = subsetPoints[0];
                var maximumProfitAllowedPerWeek = 70000;
                long weeklyProfit = 0;

                foreach (var item in subsetPoints)
                {
                    if ((int)(item.RegisterDate - oldestRegisteredPoint.RegisterDate).TotalDays <= 7)
                    {
                        weeklyProfit += CalculatePointProfit(item.Point);
                        if (weeklyProfit < maximumProfitAllowedPerWeek)
                        {
                            userProfit += CalculatePointProfit(item.Point);
                        }
                        else
                        {
                            userProfit = maximumProfitAllowedPerWeek;
                        }
                    }
                    else
                    {
                        oldestRegisteredPoint = item;
                        weeklyProfit = 0;
                        weeklyProfit += CalculatePointProfit(item.Point);
                        if (weeklyProfit < maximumProfitAllowedPerWeek)
                        {
                            userProfit += CalculatePointProfit(item.Point);
                        }
                        else
                        {
                            userProfit = maximumProfitAllowedPerWeek;
                        }
                    }
                }
            }
            userProfit -= totalUserPayment;
            return userProfit;
        }
        public long CalculateUserProfit(User model)
        {
            long userProfit = 0;
            var user = model;
            //var latestUserPayment = GetLatestUserPayment(user.Id);
            var totalUserPayment = GetTotalUserPaymentAmount(user.Id);
            var subsetWingWithLessPoints = GetUsersWeakerSubset(user);
            if (subsetWingWithLessPoints != null)
            {
                var subsetPoints = GetUserPoints(subsetWingWithLessPoints);
                var subsetChildrenPoints = GetUserSubsetPoints(subsetWingWithLessPoints);
                subsetPoints.AddRange(subsetChildrenPoints);

                //if (latestUserPayment != null)
                //    subsetPoints = subsetPoints.Where(u => u.RegisterDate > latestUserPayment.Date).ToList();

                // Maximum Profit Control
                subsetPoints = subsetPoints.OrderBy(u => u.RegisterDate).ToList();
                var oldestRegisteredPoint = subsetPoints[0];
                var maximumProfitAllowedPerWeek = 70000;
                long weeklyProfit = 0;

                foreach (var item in subsetPoints)
                {
                    if ((int)(item.RegisterDate - oldestRegisteredPoint.RegisterDate).TotalDays <= 7)
                    {
                        weeklyProfit += CalculatePointProfit(item.Point);
                        if (weeklyProfit < maximumProfitAllowedPerWeek)
                        {
                            userProfit += CalculatePointProfit(item.Point);
                        }
                        else
                        {
                            userProfit = maximumProfitAllowedPerWeek;
                        }
                    }
                    else
                    {
                        oldestRegisteredPoint = item;
                        weeklyProfit = 0;
                        weeklyProfit += CalculatePointProfit(item.Point);
                        if (weeklyProfit < maximumProfitAllowedPerWeek)
                        {
                            userProfit += CalculatePointProfit(item.Point);
                        }
                        else
                        {
                            userProfit = maximumProfitAllowedPerWeek;
                        }
                    }
                }
            }
            userProfit -= totalUserPayment;
            return userProfit;
        }
        public long CalculatePointProfit(float point)
        {
            var exchangeRate = 300;
            var profit = (long)Math.Floor(point * exchangeRate);
            return profit;
        }
    }
}